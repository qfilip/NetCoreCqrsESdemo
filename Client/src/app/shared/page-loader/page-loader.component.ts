import { Component, OnInit } from '@angular/core';
import { PageLoaderService } from 'src/app/services/page-loader.service';
import { Subscription } from 'rxjs';
import { PageLoaderInfo } from 'src/app/_notgenerated/helpers';

@Component({
  selector: 'app-page-loader',
  templateUrl: './page-loader.component.html',
  styleUrls: ['./page-loader.component.scss']
})
export class PageLoaderComponent implements OnInit {

  constructor(private pageLoaderService: PageLoaderService) { }

  status: PageLoaderInfo;
  subsciption: Subscription;

  ngOnInit(): void {
    this.status = { loading: false, message: 'Loading...' } as PageLoaderInfo;
    
    this.subsciption = this.pageLoaderService
      .loaderInfo.subscribe(value => this.status = value);
  }

}
