import type {
  CreationOptional,
  HasManyCreateAssociationMixin,
  HasManyGetAssociationsMixin,
  HasManyRemoveAssociationMixin,
  InferAttributes,
  InferCreationAttributes,
} from "sequelize";
import { compare, genSalt, hash } from "bcrypt";
import { DataTypes, Model } from "sequelize";
import { ID_MODEL as id } from "./shared";
import { sign } from "jsonwebtoken";
import sequelize from "../database";
import Task from "./task";

export default class User
  extends Model<InferAttributes<User>, InferCreationAttributes<User>> {
  declare id: CreationOptional<number>;
  declare username: string;
  declare email: string;
  declare password: string;

  declare getTasks: HasManyGetAssociationsMixin<Task>;
  declare createTask: HasManyCreateAssociationMixin<Task>;
  declare removeTask: HasManyRemoveAssociationMixin<Task, number>;

  generateJWT() {
    return sign(
      {
        username: this.username,
        sub: this.id,
      },
      process.env.SECRET_JWT!,
      { expiresIn: "7d" },
    );
  }

  async hashPassword() {
    const password = String(this.password);
    const salt = await genSalt(12);

    this.password = await hash(password, salt);
  }

  async comparePassword(password: string) {
    const p = String(password);
    return await compare(p, this.password);
  }
}

User.init({
  id,
  username: {
    type: DataTypes.STRING(20),
    allowNull: false,
    unique: true,
    validate: {
      len: [3, 20],
      isAlphanumeric: true,
    },
  },
  email: {
    type: DataTypes.STRING(),
    allowNull: false,
    unique: true,
    validate: {
      isEmail: true,
    },
  },
  password: {
    type: DataTypes.STRING(100),
    allowNull: false,
    validate: {
      len: [6, 100],
    },
  },
}, { sequelize });

User.beforeCreate(async (user) => {
  await user.hashPassword();
});
