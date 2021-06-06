import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NotifierService } from 'angular-notifier';
import { HomeService } from 'src/app/services/home.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  formModel: FormGroup;

  constructor(private fb: FormBuilder, public service: HomeService, private router: Router, private notifier: NotifierService) { }

  ngOnInit(): void {
    if (localStorage.getItem('token') != null)
      this.router.navigateByUrl('/petition/list');

    this.formModel = this.fb.group({
      FirstName: ['', Validators.required],
      LastName: ['', Validators.required],
      Email: ['', [Validators.required, Validators.email]],
      DateOfBirth: ['', Validators.required],
      Country: ['', Validators.required], // TODO DDL
      City: ['', Validators.required],
      AddressLine: ['', Validators.required],
      ZipCode: ['', [Validators.required, Validators.maxLength(5), Validators.minLength(5)]], 
      UserName: ['', Validators.required],
      Passwords: this.fb.group({
        Password: ['', [Validators.required, Validators.minLength(4)]],
        ConfirmPassword: ['', Validators.required]
      }, { validator: this.comparePasswords })
    });
  }

  onSubmit() {
    this.service.register(this.formModel.value).subscribe(
      (res: any) => {

        if (res.succeeded) {
          this.formModel.reset();
          this.notifier.notify('success', 'New user created! Registration successful.');
          this.router.navigateByUrl('/login');
        } 
        else {
          res.errors.forEach((element: any) => {
            switch (element.code) {
              case 'DuplicateUserName':
                this.notifier.notify('error', 'Error: Username is already taken. Registration failed.');
                break;

              default:
                this.notifier.notify('error', 'Error: Registration failed.');
                break;
            }
          });
        }

      },
      err => {
        this.notifier.notify('error', 'Error: Something went wrong!');
        console.log(err);
      }
    );
  }

  comparePasswords(fb: FormGroup) {
    let confirmPswrdCtrl = fb.get('ConfirmPassword');
    
    if (confirmPswrdCtrl !== null && (confirmPswrdCtrl.errors == null || 'passwordMismatch' in confirmPswrdCtrl.errors)) {
      let pswdCtrl = fb.get('Password');

      if (pswdCtrl !== null && (pswdCtrl.value != confirmPswrdCtrl.value))
        confirmPswrdCtrl.setErrors({ passwordMismatch: true });
      else
        confirmPswrdCtrl.setErrors(null);

    }
  }

}
