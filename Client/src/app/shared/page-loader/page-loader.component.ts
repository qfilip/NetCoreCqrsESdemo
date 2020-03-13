import { Component, OnInit, OnDestroy } from "@angular/core";
import { PageLoaderService } from "src/app/services/page-loader.service";
import { Subject } from "rxjs";
import { takeUntil } from 'rxjs/operators';
import { PageLoaderInfo } from "src/app/_notgenerated/helpers";

@Component({
  selector: "app-page-loader",
  templateUrl: "./page-loader.component.html",
  styleUrls: ["./page-loader.component.scss"]
})
export class PageLoaderComponent implements OnInit, OnDestroy {
  constructor(private pageLoaderService: PageLoaderService) {}

  status: PageLoaderInfo;
  unsubscribe: Subject<any> = new Subject();

  ngOnInit(): void {
    this.status = { loading: false, message: "Loading..." } as PageLoaderInfo;

    this.pageLoaderService.pageLoaderState
      .pipe(takeUntil(this.unsubscribe))
      .subscribe(status => {
        this.status = status;
      });
  }

  ngOnDestroy() {
    this.unsubscribe.next();
    this.unsubscribe.complete();
  }
}
