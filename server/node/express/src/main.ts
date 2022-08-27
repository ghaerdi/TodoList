import app from "./app";
import dotenv from "dotenv";
import { connect, createTables } from "./database";
import "./models/association";

console.clear();

if (process.env.NODE_ENV !== "production") dotenv.config();

const { PORT, SECRET_JWT, DATABASE_URI } = process.env;

if (!PORT) throw "Need to define PORT env";
if (!SECRET_JWT) throw "Need to define SECRET_JWT env";
if (!DATABASE_URI) throw "Need to define DATABASE_URI env";

(async () => {
  await connect();
  await createTables();
  app.listen(PORT);
  console.log(`server on port ${PORT}`);
})();
