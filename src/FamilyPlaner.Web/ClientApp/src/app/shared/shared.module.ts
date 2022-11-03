import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { TranslateModule, TranslatePipe } from '@ngx-translate/core';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [NotFoundComponent],
  imports: [CommonModule, FormsModule, TranslateModule],
  providers: [],
  exports: [NotFoundComponent, TranslateModule],
})
export class SharedModule {}
