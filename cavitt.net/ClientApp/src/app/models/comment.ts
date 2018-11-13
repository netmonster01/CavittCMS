//export class Comment {
//  commentId: string;
//  message: string;
//  likes: number;
//}

export class Comment {
  public commentId: number;
  public postId: number;
  public message: string;
  public userId: string;
  public userName: string;
  public dateCreated: Date;
  public dateModified: Date;
  public likes: number;
  public dislikes: number;
}
