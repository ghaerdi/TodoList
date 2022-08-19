import { login, logout, register } from "../controllers/user";
import { allowLoggedUser } from "../middleware/auth";
import { getTask } from "../middleware/task";
import { Router } from "express";
import {
  createTask,
  editTask,
  getTasks,
  removeTask,
} from "../controllers/task";
import welcome from "../controllers/home";

const router = Router();

router
  .route("/")
  .get(welcome);

// USER
router
  .route("/register")
  .post(register);

router
  .route("/login")
  .post(login);

router
  .route("/logout")
  .get(logout);

// TASK
router
  .route("/task/list")
  .all(allowLoggedUser)
  .get(getTasks);

router
  .route("/task")
  .all(allowLoggedUser)
  .post(createTask);

router
  .route("/task/:id")
  .all(allowLoggedUser, getTask)
  .patch(editTask)
  .delete(removeTask);

export default router;
