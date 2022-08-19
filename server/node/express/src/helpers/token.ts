import type { JwtPayload } from "jsonwebtoken";
import type { Request } from "express";
import { verify as JwtVerify } from "jsonwebtoken";
import User from "../models/user";

export interface TokenPayload extends JwtPayload {
  error?: boolean;
}

export class Token {
  static extract(cookies: Request["cookies"]) {
    if (!cookies) return null;

    return cookies["access_token"];
  }

  static exist(cookies: Request["cookies"]) {
    return Boolean(this.extract(cookies));
  }

  static verify(cookies: Request["cookies"]): TokenPayload | string {
    const token = this.extract(cookies)!;

    try {
      return JwtVerify(token, process.env.SECRET_JWT!);
    } catch (error: unknown) {
      console.log(error);
      return { error: true };
    }
  }

  static async isValid(cookies: Request["cookies"]) {
    if (!this.exist(cookies)) return false;

    const { sub, error } = this.verify(cookies) as TokenPayload;
    if (error) return false;

    const user = await User.findByPk(sub);
    if (!user) return false;

    return true;
  }
}
