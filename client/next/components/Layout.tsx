interface LayoutProps {
  children: JSX.Element | JSX.Element[]
}

function Layout({ children }: LayoutProps) {
  return (
    <main className="grid justify-center items-center bg-violet-500 w-screen h-screen">
      {children}
    </main>
  )
}

export default Layout;
