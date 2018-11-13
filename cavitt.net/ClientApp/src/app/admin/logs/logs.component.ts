import { Component, OnInit } from '@angular/core';
import { AuthService, ANONYMOUS_USER, LoggerService } from '../../services';
import { Log } from '../../models';

@Component({
  selector: 'app-logs',
  templateUrl: './logs.component.html',
  styleUrls: ['./logs.component.css']
})
export class LogsComponent implements OnInit {


  constructor(private loggerService: LoggerService) { }
  logs: Log[] = [];
  searchText: string;

  ngOnInit() {

    this.getLogs();

  }

  getLogs() {
    this.loggerService.getLogs().subscribe((logs: Log[]) => this.processLogs(logs));
  }

  processLogs(logs: Log[]): void {

    if (logs != null && logs.length > 0) {
      for (let log of logs) {
        this.logs.push(log);
      }
    }
  }

}
