import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, ParamMap} from '@angular/router';
@Component({
  selector: 'app-employee-list',
  template: `
  <h3>
    Employees List
  </h3>
  <ul class="items">
    <li *ngFor="let Emp of Employees" [class.selected]="isSelected(Emp)" (click)="onSelect(Emp)">
      <span class="badge">{{Emp.id}}</span> {{Emp.name}}
    </li>
</ul>
`,
  styles: []
})
export class EmployeeListComponent implements OnInit {

  public selectedId;
  Employees = [
    {"id": 1, "name": "NareshReddy"},
    {"id": 2, "name": "Swathi"},
    {"id": 3, "name": "Himachal"},
    {"id": 4, "name": "Mahesh"},
    {"id": 5, "name": "Priya"}
  ]
  constructor(private router: Router, private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.paramMap.subscribe((params: ParamMap) => {
      let id = parseInt(params.get('id'));
      this.selectedId = id;
    
    } );
  }

  onSelect(Emp) {
    
     this.router.navigate([Emp.id], { relativeTo: this.route });
  }

  isSelected(Emp) { return Emp.id === this.selectedId; }
}
