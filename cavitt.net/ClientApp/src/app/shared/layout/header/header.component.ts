import { Component, OnInit } from '@angular/core';
import { UserService, AuthService, ANONYMOUS_USER, LoggerService } from '../../../services';
import { User } from '../../../models';
import { Observable } from 'rxjs';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})

export class HeaderComponent implements OnInit {

  constructor(private _userService: UserService, private auth: AuthService, private router: Router, private route: ActivatedRoute, private loggerService: LoggerService) { }
  isAuthenticated: boolean = false;

  user: User = ANONYMOUS_USER;

  isLoggedIn: boolean = false;
  isLoggedOut: boolean = false;
  errorCount: number;

  ngOnInit() {
    this.auth.isAuthenticated().subscribe((data: boolean) => {
      this.isAuthenticated = data;
      this.activate();
    });
  }

  activate() {

    if (!this.isAuthenticated) {
      this.auth.logout();
    }

    // check if user is logged in.
    this.isLoggedIn = this.auth.isUserLoggedIn();

    this.auth.user$.subscribe(data => this.processData(data));
    this.auth.isLoggedIn$.subscribe(isLoggedIn => this.isLoggedIn === isLoggedIn);
    this.auth.isLoggedOut$.subscribe(isLoggedOut => this.isLoggedOut === isLoggedOut);

    if (this.isLoggedIn) {
      this.user = this.auth.loggedInUser();

      this.loggerService.getErrorLogCount().subscribe((errorCount: number) => this.processErrorCount(errorCount));
    }
    else {
     
    }

   
  }

  processErrorCount(errorCount: number): void {
    this.errorCount = errorCount;
  }

  logout() {
    this.auth.logout();
    this.router.navigateByUrl('login');
  }

  processData(data: User) {

    this.user = data;
    if (this.user.avatarImage) {
      this.user.avatarBase64 = this.user.avatarImageType + this.user.avatarImage;
    }
    else {
      this.user.avatarBase64 = '/assets/user.png';
    }
    
    console.log(data);
  }
}
