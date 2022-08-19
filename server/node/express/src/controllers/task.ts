import { Request, Response } from "express";
import messages from "../helpers/messages";
import User from "../models/user";
import Task from "../models/task";

const itemName = "Task";
const itemNameLowerCase = itemName.toLowerCase();

export async function getTasks(_req: Request, res: Response) {
  const user = res.locals.user as User;

  try {
    const tasks = await user.getTasks();

    res.json({ isError: false, message: `${tasks.length} ${itemNameLowerCase} ${messages.success.found}`, tasks });
  } catch (error: unknown) {
    console.error(error);
    res.status(500).json({ isError: true, message: messages.error.unknown, error });
  }
}

export async function createTask(req: Request, res: Response) {
  const user = res.locals.user as User;

  try {
    const task = await user.createTask(req.body)!;

    res.json({ isError: false, message: `${itemName} ${messages.success.created}`, task });
  } catch (error) {
    res.status(500).json({
      isError: true,
      message: `${messages.error.unknown} by saving a new ${itemNameLowerCase}`,
      error,
    });
  }
}

export async function editTask(req: Request, res: Response) {
  const { body } = req;
  const { title, description } = body;
  const task = res.locals.task as Task;

  try {
    if (task.title === title && task.description === description) {
      return res.status(300).json({
        isError: true,
        message: "nothing to update",
      });
    }

    await task.update(body);

    res.json({ isError: false, message: `${itemName} ${messages.success.edited}`, task });
  } catch (error: unknown) {
    console.error(error);
    res.status(500).json({
      isError: true,
      message: `${itemNameLowerCase} by editing a ${itemNameLowerCase}`,
      error,
    });
  }
}

export async function removeTask(_req: Request, res: Response) {
  const task = res.locals.task as Task;

  try {
    await task.destroy();

    res.json({ isError: false, msg: `${itemName} ${messages.success.deleted}` });
  } catch (error: unknown) {
    console.error(error);
    res.status(500).json({
      isError: true,
      message: `${itemNameLowerCase} by deleting a ${itemNameLowerCase}`,
      error,
    });
  }
}
