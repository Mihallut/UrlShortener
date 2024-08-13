import { UserInfo } from "./user-info";

export interface UrlInfo {
  id: string,
  originalUrl: string,
  shortedUrl: string,
  createdBy: UserInfo
}
