﻿using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NotesASPBlazorTask.Data.Database;
using NotesASPBlazorTask.Data.Models;

namespace NotesASPBlazorTask.Data.Services
{
    public class NoteService : INoteService
    {
        private readonly SqlDbContext _sqlDbContext;

        public NoteService(SqlDbContext sqlDbContext)
        {
            _sqlDbContext = sqlDbContext;
        }

        /// <summary>
        /// Retrieves all notes for a given user from the database.
        /// </summary>
        /// <param name="userId">The user ID to retrieve notes for.</param>
        /// <returns>A list of notes for the given user.</returns>
        public async Task<List<Note>> GetAllNotesAsync(string userId)
        {
            var allUserNotes = await _sqlDbContext.Notes.Include(u => u.User).Where(x => x.UserId == userId).ToListAsync();
            return allUserNotes;
        }

        /// <summary>
        /// Creates a new note in the database.
        /// </summary>
        /// <param name="newNote">The note to be created.</param>
        /// <returns>A message indicating the success or failure of the operation.</returns>
        public async Task<Message> CreateNoteAsync(Note newNote)
        {
            if (newNote is null)
            {
                var message = new Message { State = MessageState.Failed, MessageText = "Created note cannot be null" };
                return await Task.FromResult(message);
            }

            try
            {
                await _sqlDbContext.Notes.AddAsync(newNote);
                await _sqlDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new Message { State = MessageState.Failed, MessageText = ex.Message });
            }
            return await Task.FromResult(new Message { State = MessageState.Success, MessageText = $"Note '{newNote.Title}' has been created" });
        }

        /// <summary>
        /// Updates a note in the database
        /// </summary>
        /// <param name="editedNote">The note to be updated</param>
        /// <returns>A message indicating the success or failure of the update</returns>
        public async Task<Message> UpdateNoteAsync(Note editedNote)
        {
            if (editedNote is null)
            {
                var message = new Message { State = MessageState.Failed, MessageText = "Edited note cannot be null" };
                return await Task.FromResult(message);
            }

            try
            {
                _sqlDbContext.Notes.Update(editedNote);
                await _sqlDbContext.SaveChangesAsync(true);
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new Message { State = MessageState.Failed, MessageText = ex.Message });
            }
            return await Task.FromResult(new Message { State = MessageState.Success, MessageText = $"Note '{editedNote.Title}' has been updated" });
        }

        /// <summary>
        /// Asynchronously deletes a note from the database.
        /// </summary>
        /// <param name="deletingNote">The note to be deleted.</param>
        /// <returns>A message indicating the success or failure of the operation.</returns>
        public async Task<Message> DeleteNoteAsync(Note deletingNote)
        {
            if (deletingNote is null)
            {
                var message = new Message { State = MessageState.Failed, MessageText = "Note for deleting cannot be null" };
                return await Task.FromResult(message);
            }

            try
            {
                _sqlDbContext.Notes.Remove(deletingNote);
                await _sqlDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new Message { State = MessageState.Failed, MessageText = ex.Message });
            }
            return await Task.FromResult(new Message { State = MessageState.Success, MessageText = $"Note '{deletingNote.Title}' has been deleted" });
        }

        /// <summary>
        /// Gets a list of notes based on a given condition and userId.
        /// </summary>
        /// <param name="noteCondition">The condition to filter the notes by.</param>
        /// <param name="userId">The userId to filter the notes by.</param>
        /// <returns>A list of notes that match the given condition and userId.</returns>
        public async Task<IEnumerable<Note>> GetNotesByCondition(string noteCondition, string userId)
        {
            var userNotieces = await _sqlDbContext.Notes.Where(n => n.UserId == userId).ToListAsync();
            var filteredNotieces = userNotieces.Where(n => n.Title.Contains(noteCondition, StringComparison.OrdinalIgnoreCase) || n.Body.Contains(noteCondition, StringComparison.OrdinalIgnoreCase)).ToList();
            return filteredNotieces;
        }

        /// <summary>
        /// Gets a list of demo notes.
        /// </summary>
        /// <returns>A list of demo notes.</returns>
        public IEnumerable<Note> GetDemoNotieces()
        {
            return new List<Note>
            {
                new Note()
                {
                     Title = "Remember work!",
                     Body = "The main task of a programmer is to solve the problems of other people, and not to sit in social networks. Remember work!",
                     CreationDate = DateTime.Now
                },
                new Note()
                {
                     Title = "Remember eat!",
                     Body = "Food is a very important part of a programmer's job. Remember about it!",
                     CreationDate = DateTime.Now
                },
                new Note()
                {
                     Title = "Remember sleep!",
                     Body = "Without proper sleep, a programmer cannot do his difficult work. Remember sleep!",
                     CreationDate = DateTime.Now
                },
                new Note()
                {
                     Title = "Remember repeat!",
                     Body = "Tomorrow this whole cycle of notes will start over again.",
                     CreationDate = DateTime.Now
                }
            };
        }
    }
}
