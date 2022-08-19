import type { Request, Response } from "express";
import messages from "../helpers/messages";

export default function welcome(_: Request, res: Response) {
    return res.json(messages.success.welcome)
}
