import { Component, OnInit } from '@angular/core';
import { ApiService } from 'src/app/services/api.service';
import { ICocktailDto } from 'src/app/_generated/interfaces';

@Component({
  selector: 'app-presenter',
  templateUrl: './presenter.component.html',
  styleUrls: ['./presenter.component.scss']
})
export class PresenterComponent implements OnInit {

  constructor(private api: ApiService) { }

  cocktails: ICocktailDto[];

  ngOnInit(): void {
    this.api.getAllCocktails().subscribe(response => {console.log(response)});
  }
}
