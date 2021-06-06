import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NotifierService } from 'angular-notifier';
import { HomeService } from 'src/app/services/home.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  formModel: FormGroup;

  constructor(private fb: FormBuilder, private service: HomeService, private router: Router, private notifier: NotifierService) { }

  ngOnInit(): void {
    if (localStorage.getItem('token') != null)
      this.router.navigateByUrl('/petition/list');

    this.formModel = this.fb.group({
      UserName: ['', Validators.required],
      Password: ['', Validators.required]
    });
  }

  onSubmit() {
    this.service.login(this.formModel.value).subscribe(
      (res: any) => {
        localStorage.setItem('token', res.token);
        this.service.isAuthenticated = true;
        this.notifier.notify('success', 'Login successful');
        this.router.navigateByUrl('/petition/list');
      },
      err => {
        if (err.status == 400)
          this.notifier.notify('error', 'Incorrect username or password. Authentication failed.');
        else{
          this.notifier.notify('error', 'Error: Something went wrong!');
          console.log(err);
        }
      }
    );
  }

}
