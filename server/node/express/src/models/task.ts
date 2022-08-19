import {
  CreationOptional,
  DataTypes,
  ForeignKey,
  InferAttributes,
  InferCreationAttributes,
  Model,
  NonAttribute,
} from "sequelize";
import { ID_MODEL as id } from "./shared";
import sequelize from "../database";
import User from "./user";

export default class Task
  extends Model<InferAttributes<Task>, InferCreationAttributes<Task>> {
  declare id: CreationOptional<number>;
  declare title: string;
  declare description: string;
  declare done: CreationOptional<boolean>;
  declare ownerId: ForeignKey<User["id"]>;
  declare owner?: NonAttribute<User>;
}

Task.init({
  id,
  title: {
    type: DataTypes.STRING(),
    allowNull: false,
  },
  description: {
    type: DataTypes.STRING(),
  },
  done: {
    type: DataTypes.BOOLEAN,
    defaultValue: false,
  },
}, { sequelize });
