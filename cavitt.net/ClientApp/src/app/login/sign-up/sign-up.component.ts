import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserService, AuthService, ANONYMOUS_USER } from '../../services';
import { User, Role } from '../../models';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {

  signUpForm: FormGroup;
  loading = false;
  success = false;
  user: User = ANONYMOUS_USER;
  //  {
  //  id: null,
  //  email: null,
  //  password: null,
  //  token: undefined,
  //  roles: null,
  //  avatarImageBas64: null,
  //  firstName: null,
  //  lastName: null
  //};
  constructor(private fb: FormBuilder, private auth: AuthService) { }

  ngOnInit() {
    this.signUpForm = this.fb.group({
      email: ['', Validators.required],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      password: ['', Validators.required],
      confirmPassword: ['', Validators.required]
    }, { validator: this.passwordMatchValidator });
    this.signUpForm.valueChanges.subscribe(console.log);
  }

  passwordMatchValidator(frm: FormGroup) {
    return frm.controls['password'].value === frm.controls['confirmPassword'].value ? null : { 'mismatch': true };
  }

  async submitHandler() {
    this.loading = true;

    const formValue = this.signUpForm.value;

    try {
      // todo call api.
      this.auth.register(this.user).subscribe((user: User) => this.user = user);
      this.success = true;
    } catch (err) {
      console.error(err)
    }

    this.loading = false;
  }
}
