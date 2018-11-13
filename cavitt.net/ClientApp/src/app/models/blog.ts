import { Post } from "./post";

export class Blog {
  id: number;
  title: string;
  userId: string;
  author: string;
  url: string;
  posts: Post[];
}
