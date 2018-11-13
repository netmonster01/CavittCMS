import { Role } from "./";

export class User {

  id: string;
  email: string;
  password: string;
  token: string;
  roles: Role[];
  avatarImage: string;
  avatarBase64: string;
  avatarImageType: string;
  firstName: string;
  lastName: string;
  isAdmim: boolean; 
}
