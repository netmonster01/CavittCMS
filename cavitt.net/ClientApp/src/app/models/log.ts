import { LogType } from '../enums';

export class Log {

  userId: string;
  logType: LogType;
  source: string;
  message: string;
  dateCreated: string;

  constructor(userId: string, logType: LogType, source: string, message: string, dateCreated: string) {

    if (!(this instanceof Log)) {
      return new Log(userId, logType, source, message, dateCreated);
    }

    this.userId = userId;
    this.logType = logType;
    this.message = message;
    this.source = source;
  }
  
}
