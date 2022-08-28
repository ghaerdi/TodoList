import { useRouter } from 'next/router';
import { useEffect } from 'react';

function Home() {
  const router = useRouter();

  useEffect(() => {
    // router.push("login");
  }, [router]);

  return (
    <span>welcome</span>
  );
}

export default Home;
