import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatStepperModule } from '@angular/material/stepper';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MaterialFileInputModule } from 'ngx-material-file-input';
import { HttpClientModule } from '@angular/common/http';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { UserManualRoutingModule } from './user-manual-routing.module';
import { NgModule } from '@angular/core';
import { NewUserManual } from './new-usermanual/new-usermanual.component';
import { UserManualList } from './user-manual-list/usermanual-list.component';
import { MatTable, MatTableModule } from '@angular/material/table';

@NgModule({
    declarations: [
      NewUserManual,
      UserManualList
    ],
    imports: [
      CommonModule,
      MatTableModule,
      UserManualRoutingModule,
      CommonModule,
      FormsModule,  
      ReactiveFormsModule,
      MatPaginatorModule,
      MatFormFieldModule,
      MatInputModule,
      MatStepperModule,
      MatSnackBarModule,
      MatButtonModule,
      MatIconModule,
      MatSelectModule,
      MatDatepickerModule,
      MatNativeDateModule,
      MaterialFileInputModule,
      MatProgressSpinnerModule,
      HttpClientModule,
      MatAutocompleteModule  
    ]
  })
  export class UserManualModule { }