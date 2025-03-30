import {User} from "./user";
import {Role} from "./role";

export interface UserRole {
  userId: number;
  user: User;

  roleId: number;
  role: Role;
}
