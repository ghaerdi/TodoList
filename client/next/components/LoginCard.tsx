import type { NextRouter } from "next/router";
import { FormEvent } from "react";
import { useRouter } from "next/router";
import Form, { getFormData } from "./Form";
import Button from "./Button";
import Link from "next/link";
import Input from "./Input";

async function login(event: FormEvent<HTMLFormElement>, router: NextRouter) {
  event.preventDefault();

  const response = await fetch("http://localhost:3001/login", {
    method: "POST",
    headers: {
      "content-type": "application/json"
    },
    body: JSON.stringify(getFormData(event))
  })

  const data = await response.json();

  if (!data.isError) router.push("/");
}

function LoginCard() {
  const router = useRouter();

  return (
    <Form onSubmit={e => login(e, router)}>
      <Input name="username" type="text" placeholder="Username" />
      <Input name="password" type="password" placeholder="Password" />

      <Button type="submit">Login</Button>

      <Link href="register">
        <span className="text-sm hover:cursor-pointer text-center underline text-blue-500">Create an account!</span>
      </Link>
    </Form>
  );
}

export default LoginCard;
