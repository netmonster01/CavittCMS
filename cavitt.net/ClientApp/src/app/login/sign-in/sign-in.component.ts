import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService, ANONYMOUS_USER } from '../../services';
import { User} from '../../models';
import { Router, ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent implements OnInit {

  loading: boolean;
  success: boolean;
  signInForm: FormGroup;
  return = '';

  user: User = ANONYMOUS_USER;
  //user: User = {
  //  password: null,
  //  email: null,
  //  token: undefined,
  //  id: null,
  //  roles: null,
  //  avatarImageBas64: null,
  //  firstName: null,
  //  lastName: null
  //};

  constructor(private fb: FormBuilder,
    private auth: AuthService,
    private router: Router,
    private route: ActivatedRoute) { }

  ngOnInit() {

    this.signInForm = this.fb.group({
      email: ['', Validators.required],
      password: ['', Validators.required],
    });

    this.signInForm.valueChanges.subscribe(console.log);
    this.auth.user$.subscribe(data => this.processData(data));
  }

  async logInHandler() {
    this.loading = true;

    const formValue = this.signInForm.value;

    try {
      // todo call api.
      this.auth.loginAsync(this.user.email, this.user.password);//.subscribe((user: User) => this.user = user);
     
      //this.auth.login(this.user.email, this.user.password).subscribe((user: User) => this.user = user);
      this.success = true;
    } catch (err) {
      console.error(err)
    }

    this.loading = false;
  }

  processData(data: User) {
    // this.user = data;
    if (data.token) {
      this.router.navigateByUrl(this.return);
    }
    //console.log(data);
  }

}
