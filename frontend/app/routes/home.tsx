import type { Route } from "./+types/home";
import { Chat } from "../welcome/welcome";

export function meta({}: Route.MetaArgs) {
  return [
    { title: "Ask Ian" },
    { name: "description", content: "Welcome to React Router!" },
  ];
}

export default function Home() {
  return <Chat />;
}
