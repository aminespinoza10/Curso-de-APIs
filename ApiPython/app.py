import os
import pyodbc, struct
import datetime
from typing import Union
from fastapi import FastAPI
from pydantic import BaseModel

class Task(BaseModel):
    id: int
    description: str
    dateCreated: datetime.datetime
    isComplete: bool
    dateCompleted: datetime.datetime
    
connection_string = os.environ["AZURE_SQL_CONNECTIONSTRING"]

app = FastAPI()

@app.get("/all")
def get_tasks():
    rows = []
    with get_conn() as conn:
        cursor = conn.cursor()
        cursor.execute("SELECT * FROM dbo.tasks")

        for row in cursor.fetchall():
            print(row.id, row.description, row.dateCreated, row.isComplete, row.dateCompleted)
            rows.append(f"{row.id}, {row.description}, {row.dateCreated}, {row.isComplete}, {row.dateCompleted}")
    return rows

@app.post("/task")
def create_task(item:Task):
    with get_conn() as conn:
        cursor = conn.cursor()
        cursor.execute(f"INSERT INTO tasks (description, dateCreated, isComplete, dateCompleted) VALUES (?, ?, ?, ?)", item.description, item.dateCreated, item.isComplete, item.dateCompleted)
        conn.commit()
    return item

@app.put("/task/{id}")
def update_task(id:int, item:Task):
    with get_conn() as conn:
        cursor = conn.cursor()
        cursor.execute(f"UPDATE tasks SET description = ?, dateCreated = ?, isComplete = ?, dateCompleted = ? WHERE id = ?", item.description, item.dateCreated, item.isComplete, item.dateCompleted, id)
        conn.commit()
    return item

def get_conn():
    conn = pyodbc.connect(connection_string)
    return conn