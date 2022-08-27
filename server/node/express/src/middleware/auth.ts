import type { NextFunction, Request, Response } from "express";
import type { TokenPayload } from "../helpers/token";
import { Token } from "../helpers/token";
import messages from "../helpers/messages";
import User from "../models/user";

export async function allowLoggedUser(
  { cookies }: Request,
  res: Response,
  next: NextFunction,
) {
  const token = "Token";

  if (!Token.exist(cookies)) {
    return res.status(401).json({
      isError: true,
      message: `${token} ${messages.error.required}`,
    });
  }

  const payload = Token.verify(cookies) as TokenPayload;

  if (payload.error) {
    return res.status(401).json({
      isError: true,
      message: `${token} ${messages.error.invalid}`
    });
  }

  const user = await User.findByPk(payload.sub);

  if (!user) {
    return res.status(404).json({
      isError: true,
      message: `${token} ${messages.error.invalidUserRelation}`,
    });
  }

  res.locals.user = user;

  next();
}
