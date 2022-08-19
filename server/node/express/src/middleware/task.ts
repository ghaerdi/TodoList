import { Request, Response } from "express";
import messages from "../helpers/messages";
import User from "../models/user";
import Task from "../models/task";

const itemName = "Task";
const itemNameLowerCase = itemName.toLowerCase();

export async function getTask(req: Request, res: Response, next: Function) {
  const user = res.locals.user as User;
  const id = Number(req.params.id);

  try {
    const task = await Task.findByPk(id);

    if (task?.ownerId !== user.id) {
      return res.status(400).json({ isError: true, msg: `${itemName} ${messages.error.notFound}` });
    }

    res.locals.task = task;

    next();
  } catch (error: unknown) {
    console.error(error);
    res.status(500).json({
      isError: true,
      msg: `${messages.error.unknown} by finding a ${itemNameLowerCase}`,
      error,
    });
  }
}
