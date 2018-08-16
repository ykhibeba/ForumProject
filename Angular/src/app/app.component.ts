import {Component} from '@angular/core';
import {BreakpointObserver, Breakpoints, BreakpointState} from '@angular/cdk/layout';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {

  constructor(private breakpointObserver: BreakpointObserver) {
  }

  public isHandset(): Observable<boolean> {
    return this.breakpointObserver.observe(Breakpoints.Handset).pipe(map(result => result.matches));
  }

}
