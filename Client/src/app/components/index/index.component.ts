import { Component, OnInit } from '@angular/core';
import { PageLoaderService } from 'src/app/services/page-loader.service';

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.scss']
})
export class IndexComponent implements OnInit {

  constructor(private pageLoaderService: PageLoaderService) { }

  ngOnInit(): void {
  }

  test() {
    this.pageLoaderService.show();
  }

  test2() {
    this.pageLoaderService.hide();
  }
}
