import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NotifierService } from 'angular-notifier';
import { PetitionService } from 'src/app/services/petition.service';

@Component({
  selector: 'app-my-petition-list',
  templateUrl: './my-petition-list.component.html',
  styleUrls: ['./my-petition-list.component.css']
})
export class MyPetitionListComponent implements OnInit {

  petitionList: any[] = [];
  p: number = 1;

  constructor(private router: Router, private service: PetitionService, private notifier: NotifierService) { }

  ngOnInit(): void {

    // TODO enable loading

    this.service.getMyList().subscribe(
      (res: any) => {
        this.petitionList = res.result;

        // TODO disable loading
      },
      err => {
        this.notifier.notify('error', 'Error: Something went wrong!');
        console.log(err);
      },
    );

  }

}
