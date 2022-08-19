const success = Object.freeze({
  welcome: "Welcome to Tasker",
  created: "created successfully",
  deleted: "deleted successfully",
  edited: "edited successfully",
  found: "found",
});

const error = Object.freeze({
  alreadyExist: "already exists",
  unknown: "unknown error",
  notFound: "not found",
  required: "is required",
  invalid: "is invalid",
  invalidUserRelation: "does not correspond to a valid user"
});

const messages = Object.freeze({ success, error })

export default messages;
