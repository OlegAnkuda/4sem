# Generated by Django 5.0.4 on 2024-05-28 00:06

import django.core.validators
import django.db.models.deletion
import django.utils.timezone
from django.db import migrations, models


class Migration(migrations.Migration):

    dependencies = [
        ('zoo_shop_app', '0008_rename_promocode_promocode_code_and_more'),
    ]

    operations = [
        migrations.CreateModel(
            name='Review',
            fields=[
                ('id', models.BigAutoField(help_text='Unique id', primary_key=True, serialize=False)),
                ('created', models.DateTimeField(default=django.utils.timezone.now, help_text='Creation date')),
                ('update', models.DateTimeField(default=django.utils.timezone.now, help_text='Update date')),
                ('text', models.TextField(help_text='Review text')),
                ('rate', models.IntegerField(default=10, help_text='Rate from 1 to 10', validators=[django.core.validators.MinValueValidator(1), django.core.validators.MaxValueValidator(10)])),
                ('author', models.ForeignKey(help_text='Client who left review', on_delete=django.db.models.deletion.CASCADE, to='zoo_shop_app.client')),
            ],
            options={
                'db_table': 'reviews',
                'ordering': ['id'],
            },
        ),
        migrations.DeleteModel(
            name='Feedback',
        ),
    ]
