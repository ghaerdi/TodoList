import type { AppProps } from 'next/app';
import Layout from "../components/Layout";
import Head from 'next/head'
import "../styles/global.css";

export default function MyApp({ Component, pageProps }: AppProps) {
  return (
    <>
      <Head>
        <title>TodoList</title>
        <meta name="description" content="Your todolist" />
        <link rel="icon" href="/favicon.ico" />
      </Head>

      <Layout>
        <Component {...pageProps} />
      </Layout>
    </>
  )
}
