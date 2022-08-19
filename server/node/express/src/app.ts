import errorHandler from "./middleware/errorHandler";
import cookieParser from "cookie-parser";
import router from "./routes/index";
import express from "express";
import morgan from "morgan";
import cors from "cors";

const app = express();

app.use(morgan("dev"));
app.use(cors());
app.use(cookieParser());
app.use(express.urlencoded({ extended: true }));
app.use(express.json());
app.use("/", router);

app.use(errorHandler);

export default app;
