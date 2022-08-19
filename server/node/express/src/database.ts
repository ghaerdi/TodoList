import { Sequelize } from "sequelize";
import dotenv from "dotenv";

const isProduction = process.env.NODE_ENV === "production";

if (!isProduction) dotenv.config();

const {
  DATABASE_URI,
  FORCE_SYNC,
} = process.env;

if (!DATABASE_URI) {
  throw "Need to provide a DATABASE_URI env";
}

const sequelize = new Sequelize(DATABASE_URI, { logging: false });

export async function connect() {
  try {
    await sequelize.authenticate();
    if (!isProduction) console.log("✅ Database connected successfully");
  } catch (error) {
    if (!isProduction) console.error("❌ Unable to connect to the database:", error);
  }
}

export async function createTables() {
  const n = !FORCE_SYNC ? 0 : Number(FORCE_SYNC);
  if (!isProduction && (n < 0 || n > 1 || isNaN(n))) {
    throw "FORCE_SYNC must be 0 (as false) or 1 (as true)";
  }

  const force = n === 1;
  await sequelize.sync({ force });
  if (!isProduction) {
    console.log(
      force ? "⛔ Data deleted. Tables created" : "✅ Tables created",
    );
  }
}

export default sequelize;
