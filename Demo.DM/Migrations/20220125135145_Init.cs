﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Demo.DM.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "MySchema1");

            migrationBuilder.CreateTable(
                name: "T_Articles",
                columns: table => new
                {
                    PKId = table.Column<long>(type: "BIGINT", nullable: false)
                        .Annotation("Dm:Identity", "1, 1"),
                    Title = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: false),
                    Content = table.Column<string>(type: "NVARCHAR2(8188)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Articles", x => x.PKId);
                });

            migrationBuilder.CreateTable(
                name: "T_Authors",
                columns: table => new
                {
                    Id = table.Column<long>(type: "BIGINT", nullable: false)
                        .Annotation("Dm:Identity", "1, 1"),
                    Name = table.Column<string>(type: "NVARCHAR2(8188)", nullable: false),
                    Tags = table.Column<string>(type: "NVARCHAR2(8188)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_Books",
                schema: "MySchema1",
                columns: table => new
                {
                    Id = table.Column<long>(type: "BIGINT", nullable: false)
                        .Annotation("Dm:Identity", "1, 1"),
                    Title = table.Column<string>(type: "NVARCHAR2(8188)", nullable: false),
                    PubTime = table.Column<DateTime>(type: "TIMESTAMP", nullable: true),
                    Price = table.Column<double>(type: "FLOAT", nullable: false),
                    AuthorName = table.Column<string>(type: "NVARCHAR2(8188)", nullable: false),
                    Pages = table.Column<int>(type: "INT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_Comments",
                columns: table => new
                {
                    PKId = table.Column<long>(type: "BIGINT", nullable: false)
                        .Annotation("Dm:Identity", "1, 1"),
                    ArticleId = table.Column<long>(type: "BIGINT", nullable: false),
                    Message = table.Column<string>(type: "NVARCHAR2(8188)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Comments", x => x.PKId);
                    table.ForeignKey(
                        name: "FK_T_Comments_T_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "T_Articles",
                        principalColumn: "PKId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_T_Comments_ArticleId",
                table: "T_Comments",
                column: "ArticleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_Authors");

            migrationBuilder.DropTable(
                name: "T_Books",
                schema: "MySchema1");

            migrationBuilder.DropTable(
                name: "T_Comments");

            migrationBuilder.DropTable(
                name: "T_Articles");
        }
    }
}
