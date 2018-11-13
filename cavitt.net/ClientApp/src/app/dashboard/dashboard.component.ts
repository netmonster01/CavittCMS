import { Component, OnInit } from '@angular/core';
import { DashboardService } from '../services';
import { Dashboard } from '../models';
import { Observable } from 'rxjs/Observable';
//import { DomSanitizer, SafeResourceUrl, SafeHtml } from '@angular/platform-browser';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  dashboard: Dashboard;
  constructor(private dash: DashboardService) { }

  ngOnInit() {
    this.activate();
    this.dashboard = new Dashboard();
    //this.post = '<div highlight><b>hello world</b><br><pre data-lang=\'HTML\'><code >function cool(x) {return x + 1;}</code></pre><br><pre><code highlight> public class hot(string degrees) {} </code></pre></div>';
  }

  activate(): any {
    this.dash.getDashboard().subscribe((dashboard: Dashboard) => this.processData(dashboard));//blog.getLatestPost().subscribe((post: Post) => this.processData(post));
  }

  processData(dashboardCounts: Dashboard) {
    this.dashboard = dashboardCounts;
    console.log(dashboardCounts);
  }

}
