import type { FormEvent } from "react";
import styled from "styled-components";

export function getFormData(event: FormEvent<HTMLFormElement>) {
  const formData = new FormData(event.target as HTMLFormElement);
  return Object.fromEntries(formData);
}

const Form = styled.form.attrs({
  className: "bg-orange-50 rounded p-5 grid justify-center items-center"
})``;

export default Form;
