import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NotifierService } from 'angular-notifier';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent implements OnInit {

  formModel: FormGroup;

  constructor(private fb: FormBuilder, public service: AccountService, private notifier: NotifierService) { }

  ngOnInit(): void {
    this.formModel = this.fb.group({
      CurrentPassword: ['', Validators.required],
      NewPassword: ['', Validators.required],
      ConfirmNewPassword: ['', Validators.required]
    }, { validator: this.comparePasswords });
  }

  onSubmit() {
    this.service.changePassword(this.formModel.value).subscribe(
      (res: any) => {

        if (res.succeeded) {
          this.formModel.reset();
          this.notifier.notify('success', 'Password update successful.');
        } 
        else {
          res.errors.forEach((element: any) => {
            switch (element.code) {
              case 'PasswordMismatch':
                this.notifier.notify('error', 'Current password is incorrect.');
                break;

              default:
                this.notifier.notify('error', 'Password update failed.');
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
    let confirmPswrdCtrl = fb.get('ConfirmNewPassword');
    
    if (confirmPswrdCtrl !== null && (confirmPswrdCtrl.errors == null || 'passwordMismatch' in confirmPswrdCtrl.errors)) {
      let pswdCtrl = fb.get('NewPassword');

      if (pswdCtrl !== null && (pswdCtrl.value != confirmPswrdCtrl.value))
        confirmPswrdCtrl.setErrors({ passwordMismatch: true });
      else
        confirmPswrdCtrl.setErrors(null);

    }
  }

}
