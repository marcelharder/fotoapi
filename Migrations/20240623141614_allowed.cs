﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fotoservice.Migrations
{
    /// <inheritdoc />
    public partial class allowed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AllowedToSee",
                table: "AspNetUsers",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllowedToSee",
                table: "AspNetUsers");
        }
    }
}
