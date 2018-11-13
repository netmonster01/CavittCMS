import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { Vote } from '../models';


export const VOTE_PLACEHOLDER: Vote = {
  postId: 0,
  userVote: 0,
  userId: null,
  id: 0
};

@Injectable({
  providedIn: 'root'
})
export class VoteService {

  headers: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8' });

  options = {
    headers: this.headers
  };

  constructor(private _http: HttpClient) { }

  getVote(vote: Vote) {
    
    return this._http.post<Vote>('/api/Votes/MyVote', vote, this.options)
      .catch(this.handleError);
  }

  addVote(vote: Vote) {
    return this._http.post<Vote>('/api/Votes', vote, this.options)
      .catch(this.handleError);
  }

  getAllVotes() {
    return this._http.get<Vote[]>('/api/Votes', this.options)
      .catch(this.handleError);
  }
  handleError(handleError: any): any {
    console.log(handleError);
  }
}
