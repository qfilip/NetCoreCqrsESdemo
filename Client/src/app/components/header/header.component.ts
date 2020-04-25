import { Component, OnInit, EventEmitter, Output } from "@angular/core";
import { Router } from "@angular/router";

@Component({
    selector: "app-header",
    templateUrl: "./header.component.html",
    styleUrls: ["./header.component.scss"]
})
export class HeaderComponent implements OnInit {
    constructor(private router: Router) { }

    @Output('onClicked') onClicked: EventEmitter<string> = new EventEmitter(null);

    ngOnInit(): void { }

    public backToIndex() {
        this.onClicked.emit('');
        this.router.navigate(['']);
    }
}