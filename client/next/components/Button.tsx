interface ButtonProps {
  children: string;
  type: "submit";
}

function Button({ type, children }: ButtonProps) {
  return <button className="my-1 bg-red-300" type={type}>{ children }</button>
}

export default Button;
