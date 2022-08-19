import User from "./user";
import Task from "./task";

User.hasMany(Task, {
  sourceKey: "id",
  foreignKey: "ownerId",
});
