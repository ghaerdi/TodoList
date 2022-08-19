import type { Request, Response } from "express";
import messages from "../helpers/messages";
import User from "../models/user";

const itemName = "User";

export async function register(req: Request, res: Response) {
  try {
    await User.create(req.body);

    res.status(200).json({ isError: false, message: `${itemName} ${messages.success.created}` });
  } catch (error: unknown) {
    res.status(500).json({ isError: true, error });
  }
}

export async function login(req: Request, res: Response) {
  try {
    const { username, password } = req.body;
    const user = await User.findOne({ where: { username } });

    if (!user) {
      return res.status(400).json({ isError: true, message: `${itemName} ${messages.error.notFound}` });
    }

    if (!await user.comparePassword(password)) {
      return res.status(400).json({
        isError: true,
        message: `${itemName} password ${messages.error.invalid}`,
      });
    }

    const accessToken = user.generateJWT();
    res.cookie("access_token", accessToken, { httpOnly: true })
    res.json({
      isError: false,
      message: `${itemName} logged`,
    });
  } catch (error: unknown) {
    res.status(500).json({ isError: true, error });
  }
}

export function logout(_req: Request, res: Response) {
  res.clearCookie("access_token").json({
    isError: false,
    message: `${itemName} logout`,
  });
}
