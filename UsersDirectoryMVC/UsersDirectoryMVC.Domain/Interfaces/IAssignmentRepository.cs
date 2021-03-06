﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UsersDirectoryMVC.Domain.Model;

namespace UsersDirectoryMVC.Domain.Interfaces
{
    public interface IAssignmentRepository
    {
        void DeleteAssignment(int assignmentId);

        int AddAssignment(Assignment assignment);

        Assignment GetAssignment(int assignmentId);

        IQueryable<Assignment> GetAllActiveAssignments();

        void UpdateAssignment(Assignment assignment);

        List<AssignmentTag> GetAllTagsForAssignment(int id);

        Tag GetTag(int tagId);

        List<Tag> GetAllTags();
        void DeleteTags(int id);
        void AddNewTags(AssignmentTag assignmentTags);
        void DeleteTag(int id);
        void AddTag(Tag tag);
    }
}
