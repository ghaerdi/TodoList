import type { FormEventHandler, FormEvent } from "react";

export function getFormData(event: FormEvent<HTMLFormElement>) {
  const formData = new FormData(event.target as HTMLFormElement);
  return Object.fromEntries(formData);
}

interface FormProps {
  children: JSX.Element[];
  onSubmit: FormEventHandler<HTMLFormElement>;
}

function Form({children, onSubmit }: FormProps) {
  return <form className="bg-orange-50 rounded p-5 grid justify-center items-center" onSubmit={onSubmit}>{children}</form>
}

export default Form;
