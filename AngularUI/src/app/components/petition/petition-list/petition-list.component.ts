import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NotifierService } from 'angular-notifier';
import { PetitionService } from 'src/app/services/petition.service';

@Component({
  selector: 'app-petition-list',
  templateUrl: './petition-list.component.html',
  styleUrls: ['./petition-list.component.css']
})
export class PetitionListComponent implements OnInit {

  petitionList: any[] = [];
  p: number = 1;

  constructor(private router: Router, private service: PetitionService, private notifier: NotifierService) { }

  ngOnInit(): void {

    document.querySelector('.page-loading').classList.add('active'); // Show loading

    this.service.getList().subscribe(
      (res: any) => {
        this.petitionList = res.result;
        document.querySelector('.page-loading').classList.remove('active'); // Hide loading
      },
      err => {
        document.querySelector('.page-loading').classList.remove('active'); // Hide loading
        this.notifier.notify('error', 'Error: Something went wrong!');
        console.log(err);
      },
    );
  }

}
