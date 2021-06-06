import { formatDate } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NotifierService } from 'angular-notifier';
import { AccountService } from 'src/app/services/account.service';
import { PageLoadingService } from 'src/app/services/page-loading.service';

@Component({
  selector: 'app-account-info',
  templateUrl: './account-info.component.html',
  styleUrls: ['./account-info.component.css']
})
export class AccountInfoComponent implements OnInit {

  formModel: FormGroup;

  constructor(private fb: FormBuilder, public service: AccountService, private notifier: NotifierService, private loading: PageLoadingService) { }

  ngOnInit(): void {

    this.loading.showLoading(true);

    this.service.getAccountInfo().subscribe(
      (res: any) => {
        this.formModel = this.fb.group({
          FirstName: [res.firstName, Validators.required],
          LastName: [res.lastName, Validators.required],
          UserName: [res.userName],
          Email: [res.email, [Validators.required, Validators.email]],
          DateOfBirth: [formatDate(new Date(res.dateOfBirth), 'yyyy-MM-dd', 'en'), Validators.required],
          Country: [res.country, Validators.required], // TODO DDL
          City: [res.city, Validators.required],
          AddressLine: [res.addressLine, Validators.required],
          ZipCode: [res.zipCode, [Validators.required, Validators.maxLength(5), Validators.minLength(5)]]
        });

        this.loading.showLoading(false);
      },
      err => {
        this.loading.showLoading(false);
        this.notifier.notify('error', 'Error: Something went wrong!');
        console.log(err);
      },
    );
  }

  onSubmit() {
    this.service.updateAccount(this.formModel.value).subscribe(
      (res: any) => {

        if (res.succeeded) {
          this.notifier.notify('success', 'Account info update successful');
        } 
        else {
          res.errors.forEach((element: any) => {
            switch (element.code) {
              default:
                this.notifier.notify('error', 'Account info update failed.');
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

}
