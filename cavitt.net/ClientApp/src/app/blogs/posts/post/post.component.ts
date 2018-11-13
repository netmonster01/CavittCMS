import { Component, OnInit, Input } from '@angular/core';
import { Post, User, Vote, Comment } from '../../../models';
import { BlogService, AuthService, ANONYMOUS_USER, VoteService, VOTE_PLACEHOLDER, POST_PLACEHOLDER} from '../../../services';
import { MatDialog, MatDialogConfig } from '@angular/material';
import { NewCommentDailogComponent } from '../../../dialogs';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.css']
})
export class PostComponent implements OnInit {

  private currentPost: Post = new Post();
  
  voted = false;
  isLoggedIn = false;
  showComments = false;
  voteDirection = 0;
  myVote: Vote = VOTE_PLACEHOLDER;
 
  @Input() public user: User = ANONYMOUS_USER;
  @Input() set post(post: Post) {
    
    
    this.isLoggedIn = this.auth.isUserLoggedIn();
    if (this.isLoggedIn && post != null && post.postId != null) {
      this.myVote.postId = post.postId;
      if (this.user.id == null) {
        this.user = this.auth.loggedInUser();
      }
      this.myVote.userId = this.user.id;
     
      //this.myVote.us
      this.voter.getVote(this.myVote).subscribe((vote: Vote) => this.processvVoteData(vote));
    }
    this.currentPost = post;
    //this.currentPost.content = '?';
    //this.currentPost.comments = [];
  }

  constructor(private blog: BlogService, private auth: AuthService, private voter: VoteService, private dialog: MatDialog) {
   
    //this.myVote.userVote = 0;
  }

  ngOnInit() {

  }

  processUserData(data: User): void {

    this.myVote.userId = data.id;
    if (this.isLoggedIn) {

      this.user = this.auth.loggedInUser();
      this.myVote.userId = this.user.id;
      this.myVote.postId = this.currentPost.postId;
      //this.voter.getVote(this.myVote).subscribe((vote: Vote) => this.processvVoteData(vote));
    }
  }

  upVote() {

    // need to check currect vote
    //never voted
    if (this.myVote.userVote >= 0) {
      if (this.currentPost.likes >= 0) {
        this.currentPost.likes++;
      }
      this.myVote.userVote = 1;
    }

    // voted disliked
    if (this.myVote.userVote == -1) {
      this.currentPost.likes++;
      if (this.currentPost.disLikes >= 0) {
        this.currentPost.disLikes--;
      }
      this.myVote.userVote = 1;
    }

    // update database
    this.addVote();
  }

  addVote(): any {
    this.voter.addVote(this.myVote).subscribe((vote: Vote) => this.processvVoteData(vote));
  }

  processvVoteData(vote: Vote): void {

    if (vote != null) {
      this.voteDirection = vote.userVote;
      this.myVote = vote;
    }
    

  }

  downVote() {
    // need to check currect vote
    //never voted
    if (this.myVote.userVote == 0) {
      this.currentPost.disLikes++;
      this.myVote.userVote = -1;
    }

    // voted liked
    if (this.myVote.userVote == 1) {
      if (this.currentPost.likes >= 0) {
        this.currentPost.likes--;
      }
      this.currentPost.disLikes++;
      this.myVote.userVote = -1;
    }

    // update database
    this.addVote();
  }

  openNewCommentDialog() {

    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;

    dialogConfig.data = {
      id: 1,
      hasBackdrop: false,
      width: '500px'
    };

    const dialogRef = this.dialog.open(NewCommentDailogComponent, { width: '500px', height: '500px', hasBackdrop: false, data: this.currentPost });

    dialogRef.afterClosed().subscribe(

      data => this.updateComments(data)
    );
  }

  updateComments(data: Comment): void {
    if (data != null) {
      if (data instanceof Comment) {
        this.currentPost.comments.push(data);
      }
    }
  }

  viewComments() {
    this.showComments = !this.showComments;
  }

  deletPost(postId: number) {
    return this.blog.deletePost(postId).subscribe();
  }
}
