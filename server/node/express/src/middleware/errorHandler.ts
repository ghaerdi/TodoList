import type { Response, Request, NextFunction } from "express";
import type { CustomResponse } from "../types";

export default function errorHandler(_error: TypeError, _req: Request, res: Response, _next: NextFunction) {
    console.log(_error);
    const customError: CustomResponse = {
        message: "Unknown error",
        status: 500,
        isError: true,
    }

    res.status(customError.status).send(customError);
}
