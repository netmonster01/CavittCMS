import { Component, OnInit, Injectable } from '@angular/core';
import { StatsService } from '../services';
import { Stats } from '../models/stats';


@Component({
  selector: 'app-stats',
  templateUrl: './stats.component.html',
  styleUrls: ['./stats.component.css']
})
export class StatsComponent implements OnInit {

  constructor(private _stat: StatsService) {
  }

  userName: string;
  password: string;
  token: string;
  stats: Stats[] = [];

  ngOnInit() {
    this.activate();
  }

  activate() {
     // call service and pass in the username and password.
    // retuns a token. gets
    this._stat.getStats().subscribe((data: Stats[]) => this.stats = data);
  }
}
