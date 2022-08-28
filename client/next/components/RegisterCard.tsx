import type { NextRouter } from "next/router";
import { FormEvent } from "react";
import { useRouter } from "next/router";
import Form, { getFormData } from "./Form";
import Button from "./Button";
import Link from "next/link";
import Input from "./Input";

async function register(event: FormEvent<HTMLFormElement>, router: NextRouter) {
  event.preventDefault();

  const response = await fetch("http://localhost:3001/register", {
    method: "POST",
    headers: {
      "content-type": "application/json"
    },
    body: JSON.stringify(getFormData(event))
  })

  const data = await response.json()

  if (!data.isError) router.push("login");
}

function RegisterCard() {
  const router = useRouter();

  return (
    <Form onSubmit={e => register(e, router)}>
      <Input name="email" type="email" placeholder="Email" />
      <Input name="username" type="text" placeholder="Username" />
      <Input name="password" type="password" placeholder="Password" />

      <Button type="submit">Register</Button>

      <Link href="login">
        <span className="text-sm hover:cursor-pointer text-center underline text-blue-500">Already have an account?</span>
      </Link>
    </Form>
  );
}

export default RegisterCard;
