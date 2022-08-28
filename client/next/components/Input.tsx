import type { ChangeEventHandler, ChangeEvent }  from "react";
import { useState } from "react";

export function useInput() {
  const [ value, _setValue ] = useState("");

  const setValue = (event: ChangeEvent) =>
    _setValue((event.target as HTMLInputElement).value);

  return { value, setValue };
}

interface InputProps {
  name?: HTMLInputElement["name"];
  type?: HTMLInputElement["type"];
  placeholder: HTMLInputElement["placeholder"];
  value?: HTMLInputElement["value"];
  onChange?: ChangeEventHandler<HTMLInputElement>;
}

function Input({ type, placeholder, value, onChange, name }: InputProps) {
  return <input className="bg-transparent border-2 border-gray-500 rounded p-1 px-3 my-1" name={name} type={type} placeholder={placeholder} value={value} onChange={onChange}/>
}


export default Input;
